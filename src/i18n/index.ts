import { ref } from 'vue'

export type Locale = 'ja' | 'en'

const STORAGE_KEY = 'mediadeck-site-locale'

const META_TITLE: Record<Locale, string> = {
  ja: 'MediaDeck | 高機能メディア管理アプリケーション',
  en: 'MediaDeck | Advanced Media Management Application for Windows',
}

const META_DESCRIPTION: Record<Locale, string> = {
  ja: 'MediaDeckはWindows向けの高機能メディア管理アプリケーションです。画像、動画、PDF、アーカイブファイルなどを一元管理し、高度な検索、タグ付け、メタデータ管理機能を提供します。',
  en: 'MediaDeck is an advanced media management application for Windows. Organize images, videos, PDFs, and archives in one library with fast search, tagging, and metadata management.',
}

function isLocale(value: string | null): value is Locale {
  return value === 'ja' || value === 'en'
}

function resolveInitialLocale(): Locale {
  if (typeof window === 'undefined') {
    return 'ja'
  }

  try {
    const paramLang = new URLSearchParams(window.location.search).get('lang')
    if (isLocale(paramLang)) {
      return paramLang
    }
  } catch {
    // Ignore malformed query string errors
  }

  try {
    const storedLang = window.localStorage.getItem(STORAGE_KEY)
    if (isLocale(storedLang)) {
      return storedLang
    }
  } catch {
    // Ignore storage access errors (e.g., private browsing)
  }

  const browserLang = (window.navigator.languages?.[0] ?? window.navigator.language ?? '').toLowerCase()
  return browserLang.startsWith('ja') ? 'ja' : 'en'
}

function syncDocumentMeta(lang: Locale): void {
  if (typeof document === 'undefined') {
    return
  }

  document.documentElement.lang = lang
  document.title = META_TITLE[lang]

  const metaDescription = document.querySelector('meta[name="description"]')
  if (metaDescription) {
    metaDescription.setAttribute('content', META_DESCRIPTION[lang])
  }
}

const locale = ref<Locale>(resolveInitialLocale())
syncDocumentMeta(locale.value)

function setLocale(next: Locale): void {
  locale.value = next

  if (typeof window !== 'undefined') {
    try {
      window.localStorage.setItem(STORAGE_KEY, next)
    } catch {
      // Ignore storage access errors
    }
  }

  syncDocumentMeta(next)
}

function toggleLocale(): void {
  setLocale(locale.value === 'ja' ? 'en' : 'ja')
}

export function useLocale() {
  return {
    locale,
    setLocale,
    toggleLocale,
  }
}
